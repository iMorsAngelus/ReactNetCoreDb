import React, { Component } from 'react';
import {TableHeader} from './table-header'
import {TableBody} from './table-body'
import {SearchForm} from './search/search'
import {Dialog} from './dialog/dialog'
import './table.css';

export class Table extends Component {
    render() {
        return(
            <div>
                <Dialog
                    className="tableBikes" 
                    dataDetails={this.props.dataDetails}
                    visiable={this.props.visiable} 
                    onDialogCloseClick={this.props.onDialogCloseClick}>
                </Dialog>
                <SearchForm 
                    onTextChange={this.props.onTextChange} 
                    className={this.props.className}>
                </SearchForm>
                <div className={this.props.className} >
                    <TableHeader className={this.props.className+"Header"} header={[
                        ["Name"], 
                        ["Price"],
                        ["Count of sales"], 
                        ["Image"] 
                    ]}>
                    </TableHeader> 
                    <TableBody 
                        className={this.props.className}
                        onRowClick = {this.props.onRowClick}
                        data={this.props.data}>
                    </TableBody>
                </div>
            </div>
        );
    }
}
// [
//     [[1],[1],[1],[1]],
//     [[2],[2],[2],[2]],
//     [[3],[3],[3],[3]], 
//     [[4],[4],[4],[4]]/*[<img src={"data:image/png;base64," + this.props.data.img}></img>]}*/
// ]