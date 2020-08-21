import React, { Component } from 'react';
import {PostForm} from "./PostForm";

export class FetchDepartments extends Component {
    static displayName = FetchDepartments.name
    
    constructor(props) {
        super(props);
        this.state = { departments: [], loading: true, value: null };
    }
    
    componentDidMount() {
        this.populateDepartments();
    }
    
    static renderDepartmentsTable(departments) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <td>Department Id</td>
                        <td>Department Name</td>
                        <td>Department employees</td>
                    </tr>
                </thead>
                <tbody>
                {departments.map(department => 
                    <tr key={department.departmentId}>
                        <td>{department.departmentId}</td>
                        <td>{department.name}</td>
                        <td>{this.getEmployees(department.employees)}</td>
                    </tr>
                )}
                </tbody>
            </table>
        )
    }
    
    static getEmployees(employees) {
        if (employees == null) return "Empty list";
        else return employees.map(employee => employee.name);
    }
    
    render() {
        let content = this.state.loading
        ? <p><em>Loading...</em></p>
        : FetchDepartments.renderDepartmentsTable(this.state.departments);
        
        return (
            <div>
                <h1 id='tableLabel'>Departments list</h1>
                {content}
                <PostForm />
            </div>
        )
    }

    async populateDepartments() {
        const response = await fetch('department');
        const data = await response.json();
        this.setState({ departments: data, loading: false});
    }
}
