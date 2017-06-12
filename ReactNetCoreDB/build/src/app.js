import React, { Component } from 'react';
import {Table} from './table/table';
import {Header} from './header/header';
import {Footer} from './footer/footer';
import './index.css'

    let AllBikes = [];
    let Top5Bikes = [];
    let AllBikesDetails = [];
    

class App extends Component {

  state = {
    visiable:false,
    bikes:[],
    bikesDetails:[]
  }

onDialogCloseClick = () => {
  this.setState({
    ...this.state,
    visiable: false
  });
}
onTextChange = (event) => {
  if (event.target.value.length === 0)
        this.setState({
          ...this.state,
          bikes:Top5Bikes
        })
  else this.setState({
          ...this.state,
          bikes : AllBikes.filter(bike => {
            if (bike.name.indexOf(event.target.value) !== -1)
              return bike;
            else return false;
          })
        })
}
onRowClick = (event) => {
  this.setState(
  {
    ...this.state,
    visiable:true,
    bikesDetails:AllBikesDetails.filter(details => {
      if (details.id == event.target.id)
      {
        return details;
      }else return false;
    })
  })
}
componentDidMount(){
  httpGet("/AllBikes")
      .then (response => {
        AllBikes = JSON.parse(response);
        Top5Bikes = AllBikes.slice(0,5);
        this.setState({
          ...this.state,
          bikes:Top5Bikes
        })
        return httpGet("/AllBikesDetails");
      })
      .then(responseDetails =>{
        AllBikesDetails = JSON.parse(responseDetails)
        //Cut only description without others info
        AllBikesDetails = AllBikesDetails.map(function(item){
                        item.description = item.description.description;
                        return item;
                    })
        this.setState({
          ...this.state,
          bikesDetails:AllBikesDetails
        })
      })
      .catch(error => {
        console.error(error);
      })
}
  render() {
    return (
      <div className="wrapper">
        <Header></Header>
        <Table
            onTextChange={this.onTextChange} 
            onRowClick={this.onRowClick}
            onDialogCloseClick={this.onDialogCloseClick}
            className="tableBikes" 
            data={this.state.bikes}
            dataDetails={this.state.bikesDetails}
            visiable={this.state.visiable}>
        </Table>
        <Footer></Footer>
      </div>
    );
  }
}

function httpGet(httpUrl){
  return new Promise((resolve, reject) => {
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET",httpUrl);

    xmlHttp.onreadystatechange = function(){
      if (xmlHttp.readyState === 4)
        if (xmlHttp.status === 200)
          resolve(xmlHttp.responseText);
        else reject(new Error("Data load is failed"));
    }

    xmlHttp.send(null);
  });
}

export default App;