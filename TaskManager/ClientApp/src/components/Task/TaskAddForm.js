import React, { Component } from 'react';
import Axios from "axios";

export class TaskAddForm extends Component{

  constructor(props) {
    super(props);
    this.state = {
      description: null,
      priority: null,
      severity: null,
      dateCreated: null,
      expirationDate: null,
      authorId: null
    }
  }

  render() {
    return (
      <form id="add-task__form" onSubmit={this.handleSubmit}>
        <table>
          <tr>
            <td><label>Description:</label></td>
            <td><input type="text" placeholder="Description" onChange={this.handleDescriptionChange} /></td>
          </tr>
          <tr>
            <td><label>Priority:</label></td>
            <td><input type="text" placeholder="Priority" onChange={this.handlePriorityChange} /></td>
          </tr>
          <tr>
            <td><label>Severity:</label></td>
            <td><input type="text" placeholder="Severity" onChange={this.handleSeverityChange} /></td>
          </tr>
          <tr>
            <td><label>Expiration date:</label></td>
            <td><input type="date" placeholder="Expiration date" onChange={this.handleExpirationDateChange} /></td>
          </tr>
          <tr>
            <td><label>Author id:</label></td>
            <td><input type="text" placeholder="Author id" onChange={this.handleAuthorIdChange} /></td>
          </tr>
          <tr>
            <td> </td>
            <td><button onSubmit={this.handleSubmit}>Add task</button></td>
          </tr>
        </table>
      </form>
    )
  }

  handleDescriptionChange = (event) => {
    this.setState({ description: event.target.value });
  }

  handlePriorityChange = (event) => {
    this.setState({ priority: parseInt(event.target.value) });
  }

  handleSeverityChange = (event) => {
    this.setState({ severity: parseInt(event.target.value) });
  }


  handleExpirationDateChange = (event) => {
    this.setState({ dateCreated: new Date(Date.now()).toISOString()});
    this.setState({ expirationDate: new Date(Date.parse(event.target.value)).toISOString() });
  }

  handleAuthorIdChange = (event) => {
    this.setState({ authorId: parseInt(event.target.value) });
  }

  handleSubmit = (event) => {
    document.getElementById("add-task__form").reset();
    event.preventDefault();
    this.sendData();
  }

  sendData = async () => {
    const data = JSON.stringify(this.state);
    const options = {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: data
    };
    await fetch('/api/task', options);
  }
}
