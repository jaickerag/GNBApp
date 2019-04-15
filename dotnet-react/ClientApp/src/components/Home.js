import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>International Business Men</h1>
        <p>Aplicación que permite hacer seguimiento a las ventas de la empresa Gloiath National Bank. Herramientas utilizadas:</p>
        <ul>
          <li>Net Core y Web API</li>
          <li>Microsoft SQL Server para la persistencia de los datos</li>
          <li>Entity Framework</li>
          <li>Reactjs para FrontEnd</li>
        </ul>
        <p>Jaicker Avila - jaickerag@gmail.com</p>
      </div>
    );
  }
}
