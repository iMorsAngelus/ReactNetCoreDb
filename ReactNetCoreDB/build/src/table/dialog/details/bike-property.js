import React, { Component } from 'react';
import './bike-details.css';


export class BikeProperty extends Component {
    render() {
        return(
            <div className = {this.props.className + " property"}>
                {this.props.value}
            </div>
        )
    }
}