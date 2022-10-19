﻿namespace TodoApp.Domain
{
    public class TodoGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public List<Todo> Todos { get; set; } 
    }
}
