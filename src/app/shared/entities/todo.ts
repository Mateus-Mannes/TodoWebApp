export class Todo {
    public id: number;
    public description: string;
    public deadLine: Date | null;
    public createdAt: Date;
    public todoGroupId: number;
    public userId: number;

    constructor() {
    }
}
