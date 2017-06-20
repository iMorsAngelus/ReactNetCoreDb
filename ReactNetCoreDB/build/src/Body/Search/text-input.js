import React, { Component } from 'react';
import './search.css';

export class TextInput extends Component {
    render() {
        return(
                    <input
                        onChange={this.props.onTextChange}
                        name="search" 
                        type="text" 
                        id="search_box"
                        />
        );
    };
};
