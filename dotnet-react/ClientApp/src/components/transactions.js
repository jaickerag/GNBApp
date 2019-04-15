import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor (props) {
    super(props);
    this.state = { transactions: [], loading: true };
    this.refresh = this.refresh.bind(this);
  }

  refresh = (e) => {
    fetch('api/Data/refresh/'+ e.target.value)
      .then(response => response.json())
      .then(data => {
        this.setState({ transactions: data, loading: false });
      });
  }


  static TransTable (transactions) {
    let TOTAL = 0;
    transactions.forEach(datum => {
    TOTAL += datum.amount;
    });
    TOTAL = TOTAL.toFixed(2);
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>SKU</th>
            <th>Moneda</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          {transactions.map(transactions =>
            <tr key={transactions.id}>
              <td>{transactions.sku}</td>
              <td>EUR</td>
              <td>{transactions.amount.toFixed(2)}</td>
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
      ? <p><em>Por favor insertar un SKU...</em></p>
      : FetchData.TransTable(this.state.transactions);

    return (
      <div>
        <h1>Transacciones</h1>
        
      <label>SKU: 
        <input type="text" name="sku" value={this.state.transactions.sku}
          onChange={(e) => this.refresh(e)} 
          placeholder="SKU"
          required/>
        </label>
        {contents}
      </div>
    );
  }
}
