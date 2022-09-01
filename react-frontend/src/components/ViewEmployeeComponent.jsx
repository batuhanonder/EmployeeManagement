import React, { Component } from 'react'
import EmployeeService from '../services/EmployeeService'

class ViewEmployeeComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
            id: this.props.match.params.id,
            employee: {}
        }
    }

    componentDidMount(){
        EmployeeService.getEmployeeById(this.state.id).then( res => {
            this.setState({employee: res.data});
        })
    }

    render() {
        return (
            <div>
                <br></br>
                <div className = "card col-md-6 offset-md-3">
                    <h3 className = "text-center"> View Employee Details</h3>
                    <div className = "card-body">
                        <img src={this.state.imageurl} alt="Avatar" className='rounded mx-auto d-block rounded-circle '/>
                        <br/>
                        <div className = "row">
                            <label> Name: </label>
                            <div> { this.state.employee.Name }</div>
                        </div>
                        <div className = "row">
                            <label> Job Title: </label>
                            <div> { this.state.employee.JobTitle }</div>
                        </div>
                        <div className = "row">
                            <label> Email: </label>
                            <div> { this.state.employee.Email }</div>
                        </div>
                        <div className = "row">
                            <label> Phone: </label>
                            <div> { this.state.employee.Phone }</div>
                        </div>
                        <div className = "row">
                            <label> Image Url: </label>
                            <div><a href={ this.state.employee.ImegeUrl }> Image</a></div>
                        </div>
                    </div>

                </div>
            </div>
        )
    }
}

export default ViewEmployeeComponent
