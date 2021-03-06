import React, { Component } from 'react';
import { TableRow } from './table-row';
import './table.css';

export class TableBody extends Component {
  render() {
    return (
      <div 
        className={ this.props.className }
        onScroll = { this.props.onScroll }
        >
        {
          //Rows create
          this.props.bodyValue.map(function(item){
            //Data for cells
            let items=[];
            items.push(item.name);
            items.push(item.price);
            items.push(item.sell_count);
            items.push(<img 
                id={item.id}
                src={ (item.image === null)? "":"data:image/png;base64," + item.image}
                alt=""
                className="image"
                />
            );
            return <TableRow 
                      onRowClick={this.props.onRowClick}
                      className={this.props.className}
                      id={item.id}
                      key={item.id}
                      data={items}
                      />
          },this)
        }
      </div>
    );
  };
};