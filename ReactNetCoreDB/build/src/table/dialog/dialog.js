import React, { Component } from 'react';
import {DialogContainer} from './dialog-container'
import './dialog.css';

export class Dialog extends Component {
    render() {
        return(
            <div id={this.props.visiable? 'background':'hide'}>
                <DialogContainer 
                    bikeDetails={this.props.dataDetails} 
                    onDialogCloseClick={this.props.onDialogCloseClick}>
                </DialogContainer>
            </div>

        );
    }
}