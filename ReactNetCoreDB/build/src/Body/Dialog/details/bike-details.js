import React, { Component } from 'react';
import './bike-details.css'

export class BikeDetails extends Component {
    render() {
        return(
            <div className="bikeDetails">
                <div className="bikeDetails name">
                {
                    this.props.bikePropertyName.map(function(item){
                            return (
                                <div 
                                    key={item} 
                                    className="bikeDetails div"
                                    >
                                        {item}
                                </div>
                            );
                    })
                }
                </div>
                <div className="bikeDetails property">
                {
                    this.props.bikeProperty.map(function(item){
                            return (
                                <div 
                                    key={item} 
                                    className="bikeDetails div"
                                    >
                                        {item}
                                </div>
                            )
                    })
                }
                </div>
            </div>
        );
    };
};