import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Eintech</h1>
        <p>Search And Insert: please use the navigation bar to either search or insert.</p>
      </div>
    );
  }
}
