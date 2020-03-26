import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { persons: [], loading: true };
    }

    componentDidMount() {
        this.populatePeople();
    }

    renderTable(persons) {
        return (
            <div>
                <input onChange={this.doSearch}></input>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Created</th>
                            <th>Group</th>
                        </tr>
                    </thead>
                    <tbody>
                        {persons.map(person =>
                            <tr key={person.id}>
                                <td>{person.firstName} {person.lastName}</td>
                                <td>{person.created.split("T")[0]}</td>
                                <td>{person.groupName}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.persons);

        return (
            <div>
                <h1 id="tabelLabel">Eintech People</h1>
                <p>Search for people.</p>
                {contents}
            </div>
        );
    }

    async populatePeople() {
        const response = await fetch('person');
        const data = await response.json();
        this.setState({ persons: data, loading: false });
    }

    doSearch = async (e) => {
        let url = 'person/search/' + e.target.value;
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ persons: data, loading: false });
    }
}
