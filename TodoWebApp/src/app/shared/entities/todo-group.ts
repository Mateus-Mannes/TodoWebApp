import { Todo } from "./todo";

export class TodoGroup {
    public id: number;
    public name: string;
    public slug: string;
    public todos: Todo[]; 
}