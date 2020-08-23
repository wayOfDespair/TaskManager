import React, { Component } from 'react';
import {FetchTasks} from "./FetchTasks";
import {TaskAddForm} from "./TaskAddForm";

export class Tasks extends Component{
    
    constructor(props) {
        super(props);
    }
    
    render() {
        return (
            <div>
                <TaskAddForm />
                <FetchTasks />
            </div>
        )
    }
}