import React, { Component } from 'react';

export class TaskAddForm extends Component{

  constructor(props) {
    super(props);
    this.state = {
      description: null,
      priority: null,
      severity: null,
      dateCreated: null,
      expirationDate: null,
      authorId: null,
      isCompleted: false
    }
  }

  render() {
    return (
      <div>
        <h1>Add task form</h1>
        <form id="add-task__form" onSubmit={this.handleSubmit} style={{height: "30%", width: "50%"}}>
          <div className="form-group">
            <label htmlFor="descriptionInput">Description:</label>
            <textarea className="form-control" id="descriptionInput" rows="2" placeholder="Description"
                      onChange={this.handleDescriptionChange}/>
          </div>
          <div className="form-group">
            <label htmlFor="priorityInput">Priority:</label>
            <input className="form-control" id="priorityInput" type="text" placeholder="Priority"
                   onChange={this.handlePriorityChange}/>
          </div>
          <div className="form-group">
            <label htmlFor="severityInput">Severity:</label>
            <input className="form-control" id="severityInput" type="text" placeholder="Severity"
                   onChange={this.handleSeverityChange}/>
          </div>
          <div className="form-group">
            <label htmlFor="expirationDateInput">Expiration date:</label>
            <input className="form-control" id="expirationDateInput" type="datetime-local"
                   min={new Date(Date.now()).toISOString()}
                   onChange={this.handleExpirationDateChange}/>
          </div>
          <div className="form-group">
            <label htmlFor="authorIdInput">Author id:</label>
            <input className="form-control" id="authorIdInput" type="text" placeholder="Author id"
                   onChange={this.handleAuthorIdChange}/>
          </div>
          <button className="btn btn-primary" onSubmit={this.handleSubmit}>Add task</button>
        </form>
      </div>
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
    this.setState({ expirationDate: event.target.value });
  }

  handleAuthorIdChange = (event) => {
    this.setState({ authorId: parseInt(event.target.value) });
  }

  handleSubmit = (event) => {
    event.preventDefault();
    this.sendData().then(response => console.log(response));
    document.getElementById("add-task__form").reset();
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
