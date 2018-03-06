using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class mazeGen : MonoBehaviour
{
    class gridCell
    {
        public int x;
        public int y;
        public bool visited;
        public bool path;
        public gridCell(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.visited = false;
            this.path = false;
        }
    }
    class gridMaze
    {
        private Stack<gridCell> visitedCells;
        private gridCell currCell;
        private System.Random rnd = new System.Random();
        private gridCell[,] maze;
        private int x = 11, y = 11;
        private int startX, startY;
        public gridMaze(int x, int y)
        {
            this.visitedCells = new Stack<gridCell>();
            this.x = x;
            this.y = y;
            if(this.x%2==0) this.x++;
            if(this.y%2==0) this.y++;
            this.maze = new gridCell[this.x,this.y];
            for(int i = 0; i < this.x; i++)
            {
                for(int o = 0; o < this.y; o++)
                {
                    this.maze[i,o] = new gridCell(i,o);
                }
            }
        }
        public gridCell touch(int x, int y)
        {
            return this.maze[x,y];
        }
        public void generateMaze()
        {
            this.startX = this.rnd.Next(0,this.x/2);
            this.startY = this.rnd.Next(0,this.y/2);
            //generate(this.rnd.Next(0,this.x/2),this.rnd.Next(0,this.y/2));
            int mb = 1024;
            Thread T = new Thread(generate,mb*1024*1024);
            T.Start();
        }
        public void generateMaze(int x, int y)
        {
            this.startX = x;
            this.startY = y;
            //generate(x, y);
            int mb = 1024;
            Thread T = new Thread(generate,mb*1024*1024);
            T.Start();
        }
        private void generate(/*int startX, int startY*/)
        {
            this.currCell = this.maze[this.startX*2+1,this.startY*2+1];
            this.visitedCells.Push(this.currCell);
            visitCell(this.currCell);
        }
        private void visitCell(gridCell visitedCell)
        {
            visitedCell.visited = true;
            visitedCell.path = true;
            touch((visitedCell.x+visitedCells.Peek().x)/2,(visitedCell.y+visitedCells.Peek().y)/2).visited = true;
            touch((visitedCell.x+visitedCells.Peek().x)/2,(visitedCell.y+visitedCells.Peek().y)/2).path = true;
            this.visitedCells.Push(visitedCell);
            this.currCell = visitedCell;
            moveTo();
        }
        private void backtrack()
        {
            if(this.visitedCells.Count !=1)
            {
                this.visitedCells.Pop();
                this.currCell = this.visitedCells.Peek();
                moveTo();
            }
        }
        private void moveTo()
        {
            List<gridCell> dirs = new List<gridCell>();
            if(this.currCell.x-2 > 0 && !touch(this.currCell.x-2,this.currCell.y).visited) dirs.Add(touch(this.currCell.x-2,this.currCell.y));
            if(this.currCell.x+2 < this.x && !touch(this.currCell.x+2,this.currCell.y).visited) dirs.Add(touch(this.currCell.x+2,this.currCell.y));
            if(this.currCell.y-2 > 0 && !touch(this.currCell.x,this.currCell.y-2).visited) dirs.Add(touch(this.currCell.x,this.currCell.y-2));
            if(this.currCell.y+2 < this.y && !touch(this.currCell.x,this.currCell.y+2).visited) dirs.Add(touch(this.currCell.x,this.currCell.y+2));
            if(dirs.Count != 0)
            {
                visitCell(dirs[this.rnd.Next(0,dirs.Count)]);
            }
            else
            {
                backtrack();
            }
        }
        public void buildMaze()
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = new Vector3(this.x/2, 0, this.y/2);
            plane.transform.localScale = new Vector3(this.x/8, 0, this.y/8);
            plane.GetComponent<Renderer>().material.color = Color.white;
            for(int i = 0; i < this.x; i++)
            {
                for(int o = 0; o < this.y; o++)
                {
                    if(!this.maze[i,o].path)
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.localScale = new Vector3(1, 2, 1);
                        cube.transform.position = new Vector3(i, 1, o);
                        cube.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
            }
        }
    }
    void Start()
        {
            gridMaze maze = new gridMaze(50,50);
            maze.generateMaze();
            maze.buildMaze();
        }
}
