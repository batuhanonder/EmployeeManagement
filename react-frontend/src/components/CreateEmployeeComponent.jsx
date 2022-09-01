import React, { Component } from 'react'
import EmployeeService from '../services/EmployeeService';

class CreateEmployeeComponent extends Component {
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
    }

    componentDidMount(){
        if(this.state.id === '_add'){
            return
        }else{
            EmployeeService.getEmployeeById(this.state.id).then( (res) =>{
                let employee = res.data;
                this.setState({name: employee.Name,
                    email: employee.Email,
                    phone : employee.Phone
                });
            });
        }        
    }

     
    handleChange = e => {
        const { name, value } = e.target; 
        this.setState(state => ({
            [name]: value
        }));
    };

    saveOrUpdateEmployee = (e) => {
        e.preventDefault();
        let employee = {Id: this.state.id, Name: this.state.name, Email: this.state.email, Phone: this.state.phone, JobTitle: this.state.jobtitle, ImageUrl:this.state.imageurl};
        console.log('employee => ' + JSON.stringify(employee));

        if(this.state.id === '_add'){
            EmployeeService.createEmployee(employee).then(res =>{
                this.props.history.push('/employees');
            });
        }
    }


    cancel(){
        this.props.history.push('/employees');
    }

    getTitle(){
        if(this.state.id === '_add'){
            return <h3 className="text-center">Add Employee</h3>
        }else{
            return <h3 className="text-center">Update Employee</h3>
        }
    }
    render() {
        return (
            <div>
                <br></br>
                   <div className = "container">
                        <div className = "row">
                            <div className = "card col-md-6 offset-md-3 offset-md-3">
                                {
                                    this.getTitle()
                                }
                                <div className = "card-body">
                                    {this.state.imageurl ? <img src={this.state.imageurl} alt="Avatar" className='rounded mx-auto d-block rounded-circle '/> : <img class="card-img-top" src="https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/300px-No_image_available.svg.png" className="rounded mx-auto d-block rounded-circle"/>}
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

                                        <button className="btn btn-success" onClick={this.saveOrUpdateEmployee}>Save</button>
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

export default CreateEmployeeComponent
