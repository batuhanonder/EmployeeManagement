import React, { Component } from 'react'
import EmployeeService from '../services/EmployeeService';

class UpdateEmployeeComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
            id: this.props.match.params.id,
            name: '',
            email: '',
            phone: '',
            jobtitle: '',
            imageurl :''
        }
        this.handleChange = this.handleChange.bind(this);
        this.updateEmployee = this.updateEmployee.bind(this);
        
    }
   
    componentDidMount(){
        EmployeeService.getEmployeeById(this.state.id).then( (res) =>{
            let employee = res.data;
            this.setState({name: employee.Name,
                email: employee.Email,
                phone : employee.Phone,
                jobtitle : employee.JobTitle,
                imageurl : employee.ImageUrl
            });
        });
    }

    updateEmployee = (e) => {
        e.preventDefault();
        let employee = {Id: this.state.id, Name: this.state.name, Email: this.state.email, Phone: this.state.phone, JobTitle: this.state.jobtitle, ImageUrl:this.state.imageurl};
        console.log(employee);
        EmployeeService.updateEmployee(employee).then( res => {
            this.props.history.push('/employees');
        });
    }
    
 
    handleChange = e => {
        const { name, value } = e.target; 
        this.setState(state => ({
            [name]: value
        }));
    };

    cancel(){
        this.props.history.push('/employees');
        
    }
    
    render() {
        return (
            <div>
                <br></br>
                   <div className = "container">
                        <div className = "row">
                            <div className = "card col-md-6 offset-md-3 offset-md-3">
                                <h3 className="text-center">Update Employee</h3>
                                <div className = "card-body">
                                    <img src={this.state.imageurl} alt="Avatar" className='rounded mx-auto d-block rounded-circle '/>
                                    <br/>
                                    <form>
                                        <div className = "form-group">
                                            <label> Name: </label>
                                            <input type="text" placeholder="Name" name="name" className="form-control" 
                                                defaultValue={this.state.name} onChange={this.handleChange} />
                                        </div>
                                        <div className = "form-group">
                                            <label> Job Title: </label>
                                            <input type='phone' placeholder="Job title" name="jobtitle" className="form-control" 
                                                defaultValue={this.state.jobtitle} onChange={this.handleChange}/>
                                        </div>
                                        <div className = "form-group">
                                            <label> E-Mail: </label>
                                            <input type="email "placeholder="Email" name="email" className="form-control" 
                                                defaultValue={this.state.email} onChange={this.handleChange}  />
                                        </div>
                                        <div className = "form-group">
                                            <label> Phone: </label>
                                            <input type='phone' placeholder="Phone number" name="phone" className="form-control" 
                                                defaultValue={this.state.phone} onChange={this.handleChange}/>
                                        </div>
                                        <div className = "form-group">
                                            <label> Image url: </label>
                                            <input type='text' placeholder="http://" name="imageurl" className="form-control" 
                                                defaultValue={this.state.imageurl} onChange={this.handleChange}/>
                                        </div>
                                        <button className="btn btn-success" onClick={this.updateEmployee}>Save</button>
                                        <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                    </form>
                                </div>
                            </div>
                        </div>
                   </div>
            </div>
        )
    }
}

export default UpdateEmployeeComponent;
