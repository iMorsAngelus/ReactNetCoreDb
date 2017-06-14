import React, { Component } from 'react';
import './bike-details.css'

export class BikeDetails extends Component {
    render() {
        return(
            <div className = "bikeDetails">
                <div className="bikeDetails name">
                {
                    this.props.bikePropertyName.map(function(item,i){
                            return item;
                    })
                }
                </div>
                <div className="bikeDetails property">
                {
                    this.props.bikeProperty.map(function(item){
                            return item;
                    })
                }
                </div>
            </div>
        );
    }
}