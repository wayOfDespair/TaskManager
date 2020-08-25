import React, { Component } from 'react';

export class FetchTasks extends Component {
  static displayName = FetchTasks.name;

  constructor(props) {
    super(props);
    this.state = { tasks: [], loading: true };
  }

  componentDidMount() {
    this.populateTasks().then(response => console.log(response)).catch(reason => console.log(reason));
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderTasksTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Tasks list</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
  
  renderTasksTable = (tasks) => {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Employee</th>
            <th>Description</th>
            <th>Priority</th>
            <th>Severity</th>
            <th>Date created</th>
            <th>Expiration date</th>
          </tr>
        </thead>
        <tbody>
          {tasks.sort((a, b) => {
            return a.taskId - b.taskId;
          }).map(task =>
            <tr key={task.taskId}>
              <td>{task.taskId}</td>
              <td>{task.author == null ? '' : task.author.name}</td>
              <td>{task.description}</td>
              <td>{task.priority}</td>
              <td>{task.severity}</td>
              <td>{this.formatDate(task.dateCreated)}</td>
              <td>{this.formatDate(task.expirationDate)}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toString().slice(0, 24);
  }
  
  populateTasks = async () => {
    const response = await fetch('api/task');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
