import React, { useCallback, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';

import lectureService from '../../Services/lectureService';

import LectureActionForm from './LectureActionForms';
import LectureTable from './LectureTable';

import classes from '../Style/MainComponent.module.css';

const MainLectureComponent = () => {
  const INITIAL_LECTURE_FORM_DATA = {
    name: '',
    description: '',
    url: '',
    expectedHours: '',
    isEvent: ''
  };

  const [lectureFormData, setLectureFormData] = useState(
    INITIAL_LECTURE_FORM_DATA
  );
  const [lectures, setLectures] = useState([]);

  const INITIAL_SEARCH_STATE = {
    name: '',
    description: '',
    url: '',
    expectedHours: '',
    isEvent: ''
  };
  const [searchState, setSearchState] = useState(INITIAL_SEARCH_STATE);
  const INITIAL_ACTION_STATE = {
    action: null,
    show: false
  };
  const [actionState, setActionState] = useState(INITIAL_ACTION_STATE);
  const INITIAL_SORT_STATE = {
    sort: 'name',
    descending: true
  };
  const [sortState, setSortState] = useState(INITIAL_SORT_STATE);
  const INITIAL_PAGE_STATE = {
    page: 1,
    pageSize: 2
  };
  const [pageState, setPageState] = useState(INITIAL_PAGE_STATE);
  const INITIAL_PAGINATION_INFO_STATE = {
    currentPage: 0,
    pageCount: 0,
    pageSize: 0,
    recordCount: 0
  };
  const [paginationInfo, setPaginationInfoState] = useState(
    INITIAL_PAGINATION_INFO_STATE
  );

  const fetchAllLectures = useCallback(params => {
    lectureService.fetchAllLectures(params).then(response => {
      setLectures(response.data.data.results);
      setPaginationInfoState(() => {
        const UPDATED_PAGINATION_INFO_STATE = {
          currentPage: response.data.data.currentPage,
          pageCount: response.data.data.pageCount,
          pageSize: response.data.data.pageSize,
          recordCount: response.data.data.recordCount
        };
        return UPDATED_PAGINATION_INFO_STATE;
      });
    });
  }, []);

  useEffect(() => {
    let params = {
      ...sortState,
      ...pageState,
      ...searchState
    };
    fetchAllLectures(params);
  }, [pageState, sortState, searchState, fetchAllLectures]);

  const handleSortAction = ev => {
    const sort = ev.target.parentNode.getAttribute('name');
    setSortState(previousSort => {
      const UPDATED_SORT = {
        sort,
        descending:
          sort === previousSort.sort ? !previousSort.descending : false
      };
      return UPDATED_SORT;
    });
  };

  const handlePageState = ev => {
    const action = ev.target.name;
    setPageState(prevState => {
      const UPDATED_PAGE_STATE = {
        page: action === 'prev' ? prevState.page-- : prevState.page++,
        pageSize: +prevState.pageSize
      };
      return UPDATED_PAGE_STATE;
    });
  };

  const handleSearchState = ev => {
    const field = ev.target.name;
    let value = ev.target.value;
    setSearchState(prevState => {
      let UPDATED_SEARCH_STATE = {
        ...prevState
      };
      if (field === 'isEvent') {
        value = ev.target.checked;
      }
      UPDATED_SEARCH_STATE[field] = value;
      return UPDATED_SEARCH_STATE;
    });
  };

  const handleAddState = () => {
    if (!actionState.show) {
      setLectureFormData(() => {
        return INITIAL_LECTURE_FORM_DATA;
      });
    }
    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'add',
        show: prevState.action === 'add' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleEditState = ev => {
    if (!actionState.show) {
      const id = +ev.target.childNodes[0].id;
      const lecture = lectures.find(s => s.id === id);
      setLectureFormData(() => {
        return {
          id: lecture.id,
          name: lecture.name,
          description: lecture.description,
          url: lecture.url,
          expectedHours: lecture.expectedHours,
          isEvent: lecture.isEvent
        };
      });
    }

    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'edit',
        show: prevState.action === 'edit' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleDeleteState = ev => {
    if (!actionState.show) {
      const id = +ev.target.childNodes[0].id;
      const lecture = lectures.find(s => s.id === id);
      setLectureFormData(() => {
        return {
          id: lecture.id
        };
      });
    }
    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'delete',
        show: prevState.action === 'delete' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleAddSelection = ev => {
    ev.preventDefault();
    console.log(lectureFormData);
    lectureService
      .addLecture(lectureFormData)
      .then(response => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchAllLectures(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleEditSelection = ev => {
    ev.preventDefault();
    lectureService
      .modifyLecture(lectureFormData)
      .then(response => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        setLectures(prevState => {
          let lecture = prevState.findIndex(s => s.id === lectureFormData.id);
          prevState[lecture] = response.data.data;
          return prevState;
        });
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleDeleteSelection = ev => {
    ev.preventDefault();
    lectureService
      .deleteLecture(lectureFormData)
      .then(() => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchAllLectures(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleLectureFormInput = ev => {
    const inputName = ev.target.name;
    const value = ev.target.value;
    setLectureFormData(prevState => {
      let lecture = {
        ...prevState
      };
      if (inputName === 'name') {
        lecture.name = value;
      } else if (inputName === 'description') {
        lecture.description = value;
      } else if (inputName === 'url') {
        lecture.url = value;
      } else if (inputName === 'expectedHours') {
        lecture.expectedHours = value;
      } else if (inputName === 'isEvent') {
        lecture.isEvent = ev.target.checked;
      }
      return lecture;
    });
  };

  const handleResetFilters = () => {
    setSearchState(INITIAL_SEARCH_STATE);
  };

  return (
    <div className={classes.table__container}>
      <div className={classes.student_table_actions}>
        <Button
          style={{ marginLeft: 20, minWidth: 170 }}
          variant="primary"
          onClick={handleAddState}
        >
          Add new lecture
        </Button>
      </div>
      <LectureActionForm
        formModel="lecture"
        handleLectureFormInput={handleLectureFormInput}
        handleAddSelection={handleAddSelection}
        handleEditSelection={handleEditSelection}
        handleDeleteSelection={handleDeleteSelection}
        handleAddState={handleAddState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        actionState={actionState}
        lectureFormData={lectureFormData}
      />
      <LectureTable
        handlePageState={handlePageState}
        handleSortAction={handleSortAction}
        handleSearchState={handleSearchState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        handleResetFilters={handleResetFilters}
        lectures={lectures}
        paginationInfo={paginationInfo}
        sortState={sortState}
        searchState={searchState}
      />
    </div>
  );
};

export default MainLectureComponent;
