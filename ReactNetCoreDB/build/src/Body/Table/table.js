import React, { Component } from 'react';
import { TableHeader } from './table-header';
import { TableBody } from './table-body';
import './table.css';

export class Table extends Component {
  render() {
    return (
      <div className={ this.props.className }>
        <TableHeader 
          className={ this.props.className + "Header" }
          headerValue={ this.props.headerValue } 
          />
        <TableBody 
          className={ this.props.className + "Body" }
          bodyValue={ this.props.bodyValue }
          onRowClick={ this.props.onRowClick }
          onScroll = { this.props.onScroll }
          />
      </div>
    );
  };
};