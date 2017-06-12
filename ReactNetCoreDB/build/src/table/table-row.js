import React, { Component } from 'react';
import {TableCell}  from './table-cell'
import './table.css';

export class TableRow extends Component {
    render() {
        return(
            <ul 
                onClick={this.props.onRowClick}
                id = {this.props.id}
                className={this.props.className+" ul"}>
                {
                    //Cells create
                    this.props.data.map(function(item){
                        return <TableCell 
                                    id={this.props.id}
                                    className={this.props.className} 
                                    value={item}>
                                </TableCell>;
                    }, this)
                }
            </ul>
        );
    }
}
