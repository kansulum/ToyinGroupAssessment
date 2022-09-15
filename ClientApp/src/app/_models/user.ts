import { Task } from './task';
export interface User {
  token: string;
  id: number;
  username: string;
  tasks?: Task[];
}
