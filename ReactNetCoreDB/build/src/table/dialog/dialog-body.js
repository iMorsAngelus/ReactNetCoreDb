import React, { Component } from 'react';
import './dialog.css';
import {BikeDetails} from './bike-details';

export class DialogBody extends Component {
    render() {
        return(
                <div className='dialog-body'>
                    <button 
                        className='button' 
                        onClick={this.props.onDialogCloseClick}>
                            Hide         
                    </button>
                    <BikeDetails bikeDetails={this.props.bikeDetails} />
                </div>
        );
    }
}