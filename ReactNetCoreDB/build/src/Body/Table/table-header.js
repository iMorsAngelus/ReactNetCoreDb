import React, { Component } from 'react';
import { TableRow } from './table-row';
import './table.css';

export class TableHeader extends Component {
  render() {
    return (
      <div className={ this.props.className }>
        <TableRow 
          data={ this.props.headerValue } 
          className={ this.props.className }
          />
      </div>
    );
  };
};