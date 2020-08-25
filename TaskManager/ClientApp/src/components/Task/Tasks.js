import React, { Component } from 'react';
import { FetchTasks } from "./FetchTasks";
import { TaskAddForm } from "./TaskAddForm";
import { EditTaskForm } from "./EditTaskForm"

export class Tasks extends Component{

  constructor(props) {
    super(props);
  }
    
  render() {
    return (
      <div>
      <TaskAddForm />
      <EditTaskForm />
      <FetchTasks />
      </div>
    )
  }
}
