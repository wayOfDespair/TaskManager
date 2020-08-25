import React, {Component} from 'react';

export class EditTaskForm extends Component {

  constructor(props) {
    super(props);
    this.state = {
      tasks: [],
      task: {
        taskId: null,
        description: '',
        priority: null,
        severity: null,
        dateCreated: null,
        expirationDate: null,
        authorId: null,
        isCompleted: null
      }
    }
    this.getTasksList();
  }
  
  render() {
    return (
      <div>
        <h1>Edit task form</h1>
        <form>
          <div className="form-group">
            <label htmlFor="taskIdInput">Task Id:</label>
            <input type="text" list="tasks" className="form-control" id="taskIdInput" autoComplete="off" onChange={this.handleTaskIdChange} />
            <datalist id ="tasks">
              {this.state.tasks.map((task, key) => <option key={key} value={task.taskId}/> )}
            </datalist>
          </div>
          <div className="form-group">
            <label htmlFor="taskDescriptionInput">Task Description:</label>
            <textarea rows="4" className="form-control" id="taskDescriptionInput" placeholder={"Current description: " + this.state.task.description} />
          </div>
        </form>
      </div>
    )
  }
  
  handleTaskIdChange = (event) => {
    const data = this.getTask(event.target.value)
      .then(r => {
        this.setState({ task: r });
        console.log(this.state.task);
      })
      .catch(r => {
        console.log(r);
      });
  }
  
  handleTaskDescriptionInputChange = (event) => {
    
  }
  
  getTasksList = async() => {
    const response = await fetch('api/task');
    const data = await response.json();
    this.setState({tasks: data})
    console.log(this.state.tasks);
  }
  
  getTask = async (taskId) => {
    const uri = 'api/task/' + taskId;
    const response = await fetch(uri);
    return await response.json();
  }
}
