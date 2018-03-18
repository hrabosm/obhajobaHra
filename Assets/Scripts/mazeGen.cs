using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class mazeGen : MonoBehaviour
{
    public int size = 50;
    public GameObject map;
    public NavMeshSurface surface;
    //public Transform wall;
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
        public int[,] pickablesList;
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
        
        public void buildMaze(Transform wall, GameObject map, int countOfPickables, Transform pickable, Transform exit)
        {
            for(int i = 0; i < this.x; i++)
            {
                for(int o = 0; o < this.y; o++)
                {
                    if(!this.maze[i,o].path)
                    {
                        Instantiate(wall, new Vector3(i, 0.5f, o), Quaternion.identity, map.transform);
                    }
                }
            }
            buildMazePickables(countOfPickables, pickable, map, exit);
        }
        private void buildMazePickables(int count, Transform pickable, GameObject map, Transform exit)
        {
            int j, k;
            pickablesList = new int[count,2];
            for(int i = 0;i<count;i++)
            {
                pickablesList[i,0] = j = this.rnd.Next(0,this.x/2);
                pickablesList[i,1] = k = this.rnd.Next(0,this.y/2);
                for(int f = 0;f<i;f++)
                {
                    if(pickablesList[f,0] == j && pickablesList[f,1] == k)
                    {
                        Debug.Log("Same position!");
                        pickablesList[i,0] = j = this.rnd.Next(0,this.x/2);
                        pickablesList[i,1] = k = this.rnd.Next(0,this.y/2);
                    }
                }
                Instantiate(pickable, new Vector3(pickablesList[i,0]*2 + 1, 0.005f, pickablesList[i,1]*2 + 1), Quaternion.identity, map.transform);
            }
            Instantiate(exit, new Vector3(this.x/2 - 1, 0.4f, this.y-1.51f), Quaternion.identity, map.transform);
        }
    }
    public Transform wall;
    public Transform pickable;
    public Transform exit;
    public int countOfPickables;
    void Start()
    {
        gridMaze maze = new gridMaze(size,size);
        maze.generateMaze();
        maze.buildMaze(wall, map, countOfPickables, pickable, exit);
        surface.BuildNavMesh();
    }
}
