import React, { useCallback, useEffect, useRef, useState } from 'react';
import { Button } from 'react-bootstrap';
import classes from '../Components/Style/Table.module.css';
import useQuery from '../Hooks/useQuery';

import iconMove from '../Assets/iconMove.png';

import programService from '../Services/programService';
import FormModal from '../Components/FormModal';

const ProgramDetailsPage = () => {
  const [programItems, setProgramItems] = useState([]);
  const [programProps, setProgramProps] = useState({});
  const [isEditing, setIsEditing] = useState(false);
  const [isAddingItem, setIsAddingItem] = useState(false);

  const dragItem = useRef();
  const dragOverItem = useRef();

  const handleEditState = () => {
    if (isEditing) {
      const id = query.get('id');
      fetchItems(id);
    }
    setIsEditing(prevState => !prevState);
  };

  const query = useQuery();

  const fetchItems = useCallback(id => {
    programService
      .fetchOrderedProgramItems(id)
      .then(response => {
        setProgramItems(response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    const id = query.get('id');
    if (!id) window.location.replace('/program');
    const name = query.get('name');
    const content = query.get('content');
    setProgramProps({ id, name, content });
    fetchItems(id);
  }, [fetchItems, query]);

  const handleSort = () => {
    let _programItems = [...programItems];

    const draggedItemContent = _programItems.splice(dragItem.current, 1)[0];

    _programItems.splice(dragOverItem.current, 0, draggedItemContent);

    dragItem.current = null;
    dragOverItem.current = null;

    setProgramItems(_programItems);
  };

  const mapItemsOrder = () => {
    const mappedItemsOrderList = programItems.map((item, index) => {
      const id = query.get('id');
      return {
        itemId: item.id,
        programId: +id,
        order: index + 1
      };
    });
    const patchData = { itemOrders: mappedItemsOrderList };
    return patchData;
  };

  const handleSendPostRequest = () => {
    const patchData = mapItemsOrder();

    programService
      .modifyProgramItemsOrder(patchData)
      .then(response => {
        console.log(response);
        handleEditState();
      })
      .catch(() => {
        alert('Saving changes not possible. Please try again.');
        window.location.reload();
      });
  };

  const handleEditItems = ev => {
    const itemId = ev.target.id;
    const editObj = {
      id: +query.get('id'),
      name: query.get('name'),
      content: query.get('content'),
      addLectureIds: [],
      removeLectureIds: [itemId]
    };
    programService
      .editProgram(editObj)
      .then(response => setProgramItems(response.data.data.items))
      .catch(err => console.log(err));
  };

  const handleAddItem = ev => {
    setIsAddingItem(prevState => !prevState);
  };

  const handleSubmitItems = items => {
    const editObj = {
      id: +query.get('id'),
      name: query.get('name'),
      content: query.get('content'),
      addLectureIds: [...items],
      removeLectureIds: []
    };
    programService
      .editProgram(editObj)
      .then(response => {
        setProgramItems(response.data.data.items);
        handleAddItem();
      })
      .catch(err => console.log(err));
  };

  return (
    <React.Fragment>
      {isAddingItem && (
        <FormModal
          title="Add items"
          formModel="addProgramItem"
          formType="add"
          handleState={handleAddItem}
          handleFormSubmission={handleSubmitItems}
          availableItems={programItems}
        />
      )}
      <div
        style={{
          backgroundColor: `${
            isEditing ? '#fff' : 'rgba(255, 255, 255, 0.911)'
          }`,
          borderRadius: '20px',
          padding: '20px',
          margin: '20px'
        }}
      >
        <div className="table__section table-responsive">
          <div
            style={{
              display: 'flex',
              flexDirection: 'row',
              justifyContent: 'space-between',
              padding: '20px'
            }}
          >
            {!isEditing && (
              <React.Fragment>
                <h3>{programProps.name} curriculum</h3>
                <Button onClick={handleEditState} variant="outline-primary">
                  Edit program items
                </Button>
              </React.Fragment>
            )}
            {isEditing && (
              <React.Fragment>
                <h3>Edit mode</h3>

                <div>
                  <Button
                    onClick={() => handleSendPostRequest()}
                    variant="outline-success"
                  >
                    Save changes
                  </Button>
                  <Button
                    onClick={handleAddItem}
                    style={{ marginLeft: 20 }}
                    variant="outline-primary"
                  >
                    Add item
                  </Button>
                  <Button
                    style={{ marginLeft: 20 }}
                    onClick={handleEditState}
                    variant="outline-danger"
                  >
                    Cancel
                  </Button>
                </div>
              </React.Fragment>
            )}{' '}
          </div>
          <table className="table table-striped">
            <caption>List of program items</caption>
            <thead>
              <tr>
                <th scope="col">#Order</th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Name:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Description:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>URLs:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Expected hours:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Type:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Action:</span>
                  </div>
                </th>
              </tr>
            </thead>
            <tbody>
              {programItems &&
                programItems.map((s, index) => {
                  let boldClass = s.isEvent ? classes.bold : '';
                  let rowClasses = `${classes.dragCard} ${
                    isEditing ? classes.drag : ''
                  }`;
                  return (
                    <tr
                      key={index}
                      className={rowClasses}
                      draggable={isEditing}
                      onDragStart={() => (dragItem.current = index)}
                      onDragEnter={() => (dragOverItem.current = index)}
                      onDragEnd={handleSort}
                      onDragOver={ev => ev.preventDefault()}
                    >
                      <th scope="row">
                        {isEditing ? (
                          <Button
                            style={{ minWidth: 40 }}
                            variant="outline-secondary"
                            disabled
                          >
                            <img src={iconMove} alt="icon move" />
                          </Button>
                        ) : (
                          <Button
                            style={{ minWidth: 40 }}
                            variant="success"
                            disabled
                          >
                            <span style={{ fontSize: 16 }}>{index + 1}</span>
                          </Button>
                        )}
                      </th>
                      <td className={boldClass}>{s.name}</td>
                      <td>{s.description}</td>
                      <td>
                        {s.url.split(',').map(url => {
                          return (
                            <a
                              style={{
                                textDecoration: 'none',
                                marginRight: 10
                              }}
                              href={url.trim()}
                            >
                              <Button variant="outline-primary">Link</Button>
                            </a>
                          );
                        })}
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 50 }}
                          disabled
                          variant="outline-success"
                        >
                          {s.expectedHours}h
                        </Button>
                      </td>
                      <td>
                        {!s.isEvent ? (
                          <Button
                            style={{ minWidth: 100 }}
                            variant="warning"
                            disabled
                          >
                            <strong>Lecture</strong>
                          </Button>
                        ) : (
                          <Button
                            style={{ minWidth: 100 }}
                            variant="danger"
                            disabled
                          >
                            <strong>Event</strong>
                          </Button>
                        )}
                      </td>
                      <td>
                        <Button
                          onClick={handleEditItems}
                          id={s.id}
                          variant="outline-danger"
                        >
                          Remove
                        </Button>
                      </td>
                    </tr>
                  );
                })}
            </tbody>
          </table>
        </div>
      </div>
    </React.Fragment>
  );
};

export default ProgramDetailsPage;
