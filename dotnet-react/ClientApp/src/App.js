import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { SKUList } from './components/skulist';
import { FetchData } from './components/transactions';
import { Rates } from './components/rates';



export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/skulist' component={SKUList} />
        <Route path='/transactions' component={FetchData} />
        <Route path='/rates' component={Rates} />
      </Layout>
    );
  }
}
