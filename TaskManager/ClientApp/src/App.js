import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';

import './custom.css'
import {FetchDepartments} from "./components/FetchDepartments";
import {Tasks} from "./components/Task/Tasks";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/tasks' component={Tasks} />
        <Route path='/fetch-departments' component={FetchDepartments} />
      </Layout>
    );
  }
}
