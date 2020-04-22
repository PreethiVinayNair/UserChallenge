import React, { Component } from 'react';
import AppActions from './../actions/AppActions';
import { get, post } from "../lib/http";


export class UserChallenge extends Component {
  static displayName = UserChallenge.name;

  constructor(props) {
    super(props);
    UserChallenge.update();
  }


  static update() {
    get('api/userchallenge' + '?limit=5', (data) => AppActions.updateStates([
      { statePath: 'users', data },
      { statePath: 'loading', data: false }
    ]));
  }

  renderUsersTable(history, users) {
      return (
      <table className='table'>
        <thead>
          <tr>
              <th>Id</th>
              <th>Title</th>
              <th>FirstName</th>
              <th>LastName</th>
              <th>Email</th>
              <th>DOB</th>
             <th>Phone</th>
             <th>Image</th>
              <th></th>
          </tr>
        </thead>
        <tbody>
          {users.map(u =>
            <tr key={u.id}>
              <td>{u.id}</td>
              <td>{u.title}</td>
              <td>{u.firstName}</td>
              <td>{u.lastName}</td>
              <td>{u.email}</td>
              <td> {u.dob}</td>
              <td>{u.phone}</td>
              <td><img src={u.image} alt="No image" width="100px" height="100px"/></td>
              <td><button onClick={() => history.push(`/userchallengemanager/${u.id}`)}>View</button></td>
            </tr>
          )}
        </tbody>
        <tfoot>
          <tr>
            <td colSpan="12"></td>
            <td><button onClick={() => history.push(`/userchallengemanager/_`)}>New</button></td>
          </tr>
        </tfoot>
      </table>
    );
  }



  render() {
    const { history, loading, users, user } = this.props;
   
    let contents = loading
      ? <p><em>Loading...</em></p>
      : this.renderUsersTable(history, users);


    return (
      <div>
        <div>
          <h1>User Challenge</h1>
          {contents}
        </div>
      </div>

    );
  }

}

UserChallenge.defaultProps = {
  loading: false,
  users: []
}