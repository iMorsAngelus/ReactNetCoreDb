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

class App extends Component {

state = {
  visiable:false,
  loadItems:false,
  searchValue:"",
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
  let searchValue = event.target.value.toLowerCase();
  //Find bikes
  fetch( "/FindBikes/0/" + searchValue )
    .then(response =>{
      return response.json()
    })
    .then(jsonResult => {
        this.setState({
          ...this.state,
          searchValue:searchValue,
          bikes:jsonResult
        });
    })
    .catch(error => {
      console.error(error);
    });
};
onScroll = (event) => {

  let bikeHeight = event.target.children[0].clientHeight;
  let scrollBikeCount = event.target.scrollTop/bikeHeight + 5;
  let countBikes = event.target.children.length;
  if ( !this.state.loadItems && scrollBikeCount>countBikes-3){
    
      //Set flag while item load
      this.setState({
          ...this.state,
          loadItems:true
      })

      fetch( "/FindBikes/" + countBikes + "/" + this.state.searchValue )
        .then(response =>{
          return response.json()
        })
        .then(jsonResult => {
            //Create new array => old + new 10 items
            let new_bikes = this.state.bikes;

            if (jsonResult !== null)
            {
              jsonResult.map(function(item){
                new_bikes.push(item);
                return null;
              });
            }

            this.setState({
              ...this.state,
              bikes:new_bikes,
              loadItems:false
            })
        })
        .catch(error => {
          console.error(error);
        });
  };

}
onRowClick = (event) => {
    fetch( "/BikeDetails/" + event.target.id )
    .then(response =>{
      return response.json()
    })
    .then(jsonResult => {
        this.setState({
          ...this.state,
          visiable : true,
          bikesDetails:jsonResult
        });
    })
    .catch(error => {
      console.error(error);
    });
};
componentDidMount(){
  //Recive top 5 bikes
  fetch( "/TopBikes" )
    .then(response =>{
      return response.json()
    })
    .then(jsonResult => {
        this.setState({
          ...this.state,
          bikes:jsonResult
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
          onScroll = { this.onScroll }
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
