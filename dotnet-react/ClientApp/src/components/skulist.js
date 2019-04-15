import React, { Component } from 'react';


export class SKUList extends Component {
  static displayName = SKUList.name;
  
  constructor (props) {
    super(props);
    this.state = { skulist: [], loading: true };
    this.refresh();
    this.refresh = this.refresh.bind(this);
    this.updatex = this.updatex.bind(this);
  }


  refresh(){

    fetch('api/Data/refresh')
      .then(response => response.json())
      .then(data => {
        this.setState({ skulist: data, loading: false });
        
      });
      
  }

  updatex(){
    this.setState({ skulist: [], loading: true });
    fetch('api/Data/updatex')
      .then(response => response.json())
      .then(data => {
        this.setState({ skulist: data, loading: false });
      }).catch(function(error) {
        console.log("error")}
        ).then(this.refresh);
      
  }

  
 

  static TransTable (skulist) {
    let TOTAL = 0;
    skulist.forEach(datum => {
    TOTAL += datum.amount;
    });
    TOTAL = TOTAL.toFixed(2);
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>SKU</th>
            <th>Moneda</th>
            <th>Monto</th>
          </tr>
        </thead>
        <tbody>
          {skulist.map(skulist =>
            <tr key={skulist.id}>
              <td>{skulist.sku}</td>
              <td>EUR</td>
              <td>{skulist.amount.toFixed(2)}</td>
            </tr>
          )}
          <tr>
          <td></td>
          <td><b>Total</b></td>
          <td>{TOTAL}</td>
          </tr>
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : SKUList.TransTable(this.state.skulist);

    return (
      <div>
        <h1>Listado de Ventas SKU</h1>
        <button className="btn btn-primary" onClick={this.updatex}>Actualizar</button>
        {contents}
      </div>
    );
  }
}
