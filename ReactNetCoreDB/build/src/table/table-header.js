import React, { Component } from 'react';
import {TableRow} from './table-row'
import './table.css';

export class TableHeader extends Component {
    render() {
        return(
                <TableRow 
                    className={this.props.className} 
                    key={0} 
                    data={this.props.header}>
                </TableRow>
        );
    }
}