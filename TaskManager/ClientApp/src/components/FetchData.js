import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { tasks: [], loading: true };
  }

  componentDidMount() {
    this.populateTasks();
  }

  static renderForecastsTable(tasks) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Author</th>
            <th>Description</th>
            <th>Date created</th>
            <th>Expiration date</th>
          </tr>
        </thead>
        <tbody>
          {tasks.map(task =>
            <tr key={task.id}>
              <td>{task.id}</td>
              <td>{task.author}</td>
              <td>{task.description}</td>
              <td>{task.dateCreated}</td>
              <td>{task.expirationDate}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Tasks list</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateTasks() {
    const response = await fetch('task');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
