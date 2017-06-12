import React, { Component } from 'react';
import './dialog.css';


export class BikeDetails extends Component {
    render() {
        return(
            <div>
                {
                    console.log(this.props.bikeDetails)              
                }
            </div>
        );
    }
}