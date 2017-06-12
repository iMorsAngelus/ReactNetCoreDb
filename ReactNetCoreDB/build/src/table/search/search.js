import React, { Component } from 'react';
import {TextInput} from './text-input'
import './search.css';

export class SearchForm extends Component {
    render() {
        return(
            <div id="search" className={this.props.className+" search"}>
                <form id="search_form">
                    <label id="label" >Filtering by name:</label>
                    <TextInput 
                        onTextChange = {this.props.onTextChange}
                        name="search" 
                        type="text" 
                        id="search_box"/>
                    <div id="search_button" />
                </form>
            </div>
        );
    }
}
