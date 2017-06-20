import React, { Component } from 'react';
import { SearchForm } from './Search/search';
import { Table } from './Table/table';
import { Dialog } from './Dialog/dialog';
import './body.css';

export class Body extends Component {
  render() {
    return (
      <div className="body">
        <SearchForm 
          onTextChange={ this.props.onTextChange }
          />
        <Dialog 
          visiable={ this.props.visiable }
          dataDetails={ this.props.dataDetails }
          detailsHeader={ this.props.detailsHeader }
          onDialogCloseClick={ this.props.onDialogCloseClick }
          />
        <Table 
          className={ this.props.className }
          headerValue={ this.props.headerValue }
          bodyValue={ this.props.bodyValue }
          onRowClick={ this.props.onRowClick }
          />
      </div>
    );
  };
};