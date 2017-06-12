import React, { Component } from 'react';
import './dialog.css';
import {BikeDetails} from './details/bike-details';

let detailsName=[], detailsProperty=[];
export class DialogBody extends Component {
    render() {
        return(
                <div className='dialog-body'>
                    <button 
                        className='button' 
                        onClick={this.props.onDialogCloseClick}/>        
                    {/*Data for property*/}
                    {this.props.bikeDetails
                            .slice(0,1)
                            .map(function(item){
                                detailsProperty = []; detailsName=[];
                                detailsName.push(<div className="bikeDetails div">image</div>)
                                detailsProperty.push(<img
                                        className="bikeDetails div"
                                        src = {"data:image/png;base64,"+item.image} 
                                        alt = "bikes image"/>)
                                detailsName.push(<div className="bikeDetails div">Bikes name</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.name}</div>)
                                detailsName.push(<div className="bikeDetails div">Description</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.description}</div>)
                                detailsName.push(<div className="bikeDetails div">Class</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.Class}</div>)
                                detailsName.push(<div className="bikeDetails div">Style</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.style}</div>)
                                detailsName.push(<div className="bikeDetails div"> Weight</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.weight}</div>)
                                detailsName.push(<div className="bikeDetails div"> Size </div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.size}</div>)
                                detailsName.push(<div className="bikeDetails div">color</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.color}</div>)
                                detailsName.push(<div className="bikeDetails div">Safety stock level</div>)
                                detailsProperty.push(<div className="bikeDetails div">{item.safety}</div>)
                            })
                    }
                    <BikeDetails 
                        bikePropertyName={detailsName} 
                        bikeProperty={detailsProperty} />
                </div>
        );
    }
}