import React, { Component } from 'react';
import './dialog.css';
import {BikeDetails} from './details/bike-details';

let detailsProperty=[];
export class DialogBody extends Component {
    render() {
        return(
                <div className='dialog-body'>
                    <button 
                        className='button' 
                        onClick={this.props.onDialogCloseClick}
                        />        
                    {/*Data for property*/}
                    {this.props.bikeDetails
                            .slice(0,1)
                            .map(function(item){
                                detailsProperty = [];
                                detailsProperty.push(<img
                                        className="bikeDetails div"
                                        src={"data:image/png;base64," + item.image} 
                                        alt="bike"
                                        />
                                );
                                detailsProperty.push(item.name);
                                detailsProperty.push(item.description);
                                detailsProperty.push(item.class);
                                detailsProperty.push(item.style);
                                detailsProperty.push(item.weight);
                                detailsProperty.push(item.size);
                                detailsProperty.push(item.color);
                                detailsProperty.push(item.safety);
                                return null;
                            })
                    }
                    <BikeDetails 
                        bikePropertyName={this.props.detailsHeader} 
                        bikeProperty={detailsProperty} 
                        />
                </div>
        );
    };
};