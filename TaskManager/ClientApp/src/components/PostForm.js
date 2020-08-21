import React, { Component } from 'react';
import Axios from "axios";

export class PostForm extends Component{
    constructor(props) {
        super(props);
        this.state = {name: "Enter department name"};
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    
    handleChange(event) {
        this.setState({name: event.target.value})
    }
    
    handleSubmit(event) {
        const jsonString = JSON.stringify(this.state)
        event.preventDefault();
        console.log(jsonString);
        Axios.post("https://localhost:50001/department", jsonString, {headers: {"Content-Type": "application/json"}})
            .then(response => {
                console.log(response);
                window.location.reload();
            })
            .catch(error => {
                console.log(error);
                alert("Unable to post data")
            });
    }
    
    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label >
                    Department name
                    <input type="text" name="name" value={this.state.value} onChange={this.handleChange} />
                </label>
                <input type="submit" value="Add department"/>
            </form>
        )
    }
}
