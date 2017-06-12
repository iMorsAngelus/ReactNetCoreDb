import React, { Component } from 'react';
import './bike-details.css'

export class BikePropertyName extends Component {
    render() {
        return(
            <div className = {this.props.className + " name"}>
                {this.props.name}
            </div>
        )
    }
}