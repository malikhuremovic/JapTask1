import searchIcon from '../Assets/searchIcon.png';
import sortIconDesc from '../Assets/sortIconAsc.png';

import classes from './ProgramTable.module.css';

const ProgramTable = ({ programs }) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>List of programs</caption>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="name" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sorticon" />{' '}
                </div>{' '}
                &nbsp; <span>Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="content" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sortIcon" />{' '}
                </div>{' '}
                &nbsp; <span>Content:</span>
              </div>
            </th>
          </tr>
          <tr>
            <th scope="col">
              <img
                className={classes.searchIcon}
                src={searchIcon}
                alt="search"
              />
            </th>
            <th scope="col">
              <input
                type="text"
                name="name"
                className="form-control"
                placeholder="Name: "
                disabled
              />
            </th>
            <th scope="col">
              <input
                name="content"
                className="form-control"
                type="text"
                placeholder="Content: "
                disabled
              />
            </th>
          </tr>
        </thead>
        <tbody>
          {programs &&
            programs.map((p, index) => {
              return (
                <tr key={p.name}>
                  <th scope="row">{index + 1}</th>
                  <td>{p.name}</td>
                  <td>{p.content}</td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default ProgramTable;
