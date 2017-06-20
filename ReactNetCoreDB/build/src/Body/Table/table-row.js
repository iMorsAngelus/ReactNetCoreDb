import React, { Component } from 'react';
import { TableCell } from './table-cell';
import './table.css';

export class TableRow extends Component {
  render() {
    return (
      <ul 
          id={ this.props.id }
          onClick={ this.props.onRowClick }
          className={ this.props.className + " ul" }
          >
          {
              //Cells create
              this.props.data.map(function(item){
                  return <TableCell 
                              key={ (item.id === undefined)? item:item.id }
                              id={ this.props.id }
                              className={ this.props.className } 
                              value={ item }
                              >
                          </TableCell>;
              }, this)
          }
      </ul>
    );
  };
};