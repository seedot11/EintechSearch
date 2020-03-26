import React, { Component } from 'react';

export class InsertData extends Component {
    static displayName = InsertData.name;

    constructor(props) {
        super(props);
        this.state = {
            firstName: "", lastName: "", groupId: 0, groups: [], loading: true
        };
    }

    componentDidMount() {
        this.populateGroups();
    }

    handleFirstName = (e) => {
        this.setState({
            firstName: e.target.value
        });
    }

    handleLastName = (e) =>  {
        this.setState({
            lastName: e.target.value
        });
    }

    handleGroup = (e) => {
        console.log(e.target.value);
        this.setState({
            groupId: parseInt(e.target.value)
        });
    }

    render() {
        return (
            <form onSubmit={this.doInsert}>
                <label>
                    First Name:
                </label>
                <input type="text" onChange={this.handleFirstName}/>    
                <label>
                    Last Name:
                </label>
                <input type="text" onChange={this.handleLastName}/>    
                <label>
                    Group:
                </label>
                <select id='groups' onChange={this.handleGroup}>
                    {this.state.groups.map(group =>
                        <option key={group.id} value={group.id}>{group.name}</option>
                    )}
                </select>  
                <input type="submit" value="Submit" />
            </form>
        );
    }

    async populateGroups() {
        const response = await fetch('person/groups');
        const data = await response.json();
        this.setState({ groups: data, groupId: data[0].id, loading: false });
    }

    doInsert = async (e) => {
        console.log(e);
        let url = 'person';
        fetch(url, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    firstName: this.state.firstName,
                    lastName: this.state.lastName,
                    groupId: this.state.groupId
                }
            )
        })
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ person: data, loading: false });
    }
}
