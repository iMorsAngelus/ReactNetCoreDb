import React, { Component } from 'react';
import './App.css';
import { Header } from './Header/header';
import { Body } from './Body/body';
import { Footer } from './Footer/footer';

const tableHeader = [
  ["Name"], 
  ["Price"],
  ["Count of sales"], 
  ["Image"]
];
const dialogDetailsHeader = [
  "Image",
  "Bikes name",
  "Description",
  "Class",
  "Style",
  "Weight",
  "Size",
  "Color",
  "Safety stock level"
];

let AllBikes = [];
let Top5Bikes = [];
let AllBikesDetails = [];

class App extends Component {

state = {
  visiable:false,
  bikes:[],
  bikesDetails:[]
};
onDialogCloseClick = () => {
  this.setState({
    ...this.state,
    visiable: false
  });
};
onTextChange = (event) => {
  let newBikes = AllBikes.slice(0,5);
  let searchValue = event.target.value.toLowerCase();
  
  if (searchValue.length !== 0){
    newBikes = AllBikes.filter(function(item){
      let bikeName = item.name.toLowerCase();
      let bikePrice = item.price.toString();
      let bikeSellCount = item.sell_count.toString();

      return (
          //Filtering by name
          bikeName.indexOf(searchValue) !== -1 
          //Filtering by price
          || bikePrice.indexOf(searchValue) !== -1
          //Filtering by count of sales
          || bikeSellCount.indexOf(searchValue) !== -1
      );
    });
  };

  this.setState({
    ...this.state,
    bikes:newBikes
  })
};
onRowClick = (event) => {
  this.setState({
    ...this.state,
    visiable : true,
    bikesDetails: AllBikesDetails.filter(function(item){
      return (event.target.id === item.id.toString())
    })
  });
};

componentDidMount(){
  //Recive all bikes
  fetch("/AllBikes")
      .then (response => {
        return response.json()
      })
      .then(jsonBikes => {
        AllBikes = jsonBikes;
        Top5Bikes = AllBikes.slice(0,5);

        this.setState({
          ...this.state,
          bikes:Top5Bikes
        });
      })
      .catch(error => {
        console.error(error);
      });

  //Recive details for all bikes
  fetch("/AllBikesDetails")
      .then(responseDetails =>{
        return responseDetails.json()
      })
      .then(jsonDetails => {
          AllBikesDetails = jsonDetails;

          this.setState({
            ...this.state,
            bikesDetails:AllBikesDetails
          })
      })
      .catch(error => {
        console.error(error);
      });
};

  render() {
    return (
      <div className="App">
        <Header />
        <Body 
          //Table
          className="table"
          headerValue={ tableHeader }
          bodyValue={ this.state.bikes }
          onRowClick={ this.onRowClick }
          onTextChange={ this.onTextChange }
          //Dialog
          visiable={ this.state.visiable }
          detailsHeader={ dialogDetailsHeader }
          dataDetails={ this.state.bikesDetails }      
          onDialogCloseClick={ this.onDialogCloseClick }
          />
        <Footer />
      </div>
    );
  };
}

export default App;
