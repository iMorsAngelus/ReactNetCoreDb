import React, { Component } from 'react';
import './table.css';

export class TableCell extends Component {
    render() {
        return(
            <li 
                id = {this.props.id}
                className={this.props.className+" li"}>
                    {this.props.value}
            </li>  
        );
    }
}