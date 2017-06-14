import React, { Component } from 'react';
import {TableRow} from './table-row'
import './table.css';

export class TableBody extends Component {
    render() {
        return(
            <div className={this.props.className+" div"}>
                {
                    //Rows create
                    this.props.data.map(function(item) {
                        //Data for cells
                        let items=[];
                        items.push(item.name);
                        items.push(item.price);
                        items.push(item.sell_count);
                        items.push(<img 
                            id = {item.id}
                            src = {"data:image/png;base64,"+item.image}
                            alt={"bike"}
                            className="image"/>
                        );
                        return <TableRow 
                                    id={item.id}
                                    key={item.id} 
                                    onRowClick = {this.props.onRowClick}
                                    className={this.props.className} 
                                    data={items} />;
                    }, this)
                }
            </div>
        );
    }
}