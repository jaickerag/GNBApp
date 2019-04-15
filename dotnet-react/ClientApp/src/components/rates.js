import React, { Component } from 'react';


export class Rates extends Component {
  static displayName = Rates.name;
  
  constructor (props) {
    super(props);
    this.state = { rateslist: [], loading: true };
    this.refresh();
    this.refresh = this.refresh.bind(this);
    this.updatex = this.updatex.bind(this);
  }


  refresh(){

    fetch('api/Data/rates')
      .then(response => response.json())
      .then(data => {
        this.setState({ rateslist: data, loading: false });
        
      });
      
  }

   handleErrors(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}

  updatex(){
    this.setState({ rateslist: [], loading: true });
    fetch('api/Data/updRates')
      .then(response => response.json())
      .then(data => {
        this.setState({ rateslist: data, loading: false });
      }).catch(function(error) {
        console.log(error)}
        ).then(this.refresh);
      
  }

  
 

  static TransTable (rateslist) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Desde</th>
            <th>Hasta</th>
            <th>Tasa</th>
          </tr>
        </thead>
        <tbody>
          {rateslist.map(rateslist =>
            <tr key={rateslist.id}>
              <td>{rateslist.fromCurr}</td>
              <td>{rateslist.toCurr}</td>
              <td>{rateslist.rate}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Rates.TransTable(this.state.rateslist);

    return (
      <div>
        <h1>Rates</h1>
        <button className="btn btn-primary" onClick={this.updatex}>Actualizar</button>
        {contents}
      </div>
    );
  }
}
