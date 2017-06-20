import React, { Component } from 'react';
import {DialogBody} from './dialog-body';
import './dialog.css';

export class DialogContainer extends Component {

    render() {
        return(
                <div className='dialog-container'>
                    <DialogBody 
                        bikeDetails={this.props.bikeDetails} 
                        detailsHeader={this.props.detailsHeader}
                        onDialogCloseClick={this.props.onDialogCloseClick}
                        >
                    </DialogBody>
                </div>
        );
    };
};