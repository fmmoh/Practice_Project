import react, { useState, useEffect } from 'react';
import BookItem from './book-item';
import ReactPaginate from 'react-paginate';


const BookList = () => {
            const[books, setBooks] = useState(null);
            const[booksCount, setBooksCount] = useState(0);
            const[page, setPage] = useState(0);

            useEffect(() => {
                // get all book
                getBooks();
            }, [page]);

            const getBooks = () => {
                fetch(process.env.REACT_APP_API_URL + "/book?pageSize=" + process.env.REACT_APP_PAGING_SIZE + "&pageIndex=" + page)
                .then(res => res.json())
                .then(res => {
                    if(res.status === true && res.data.count > 0){
                        setBooks(res.data.books);
                        setBooksCount(Math.ceil(res.data.count / process.env.REACT_APP_PAGING_SIZE));
                    }
                    if(res.data.count === 0){
                        alert("There is no book data in system.");
                    }
                })
                .catch(err => alert("Error getting data"));
            }

            const handlePageClick = (pageIndex) => {
            setPage(pageIndex.selected);
            }

            const deleteBook = (id) => {
                fetch(process.env.REACT_APP_API_URL + "/book?id=" + id, {
                    method: "DELETE",
                    headers: {
                        'Accept': 'application/json',
                        'content-Type': 'application/json',
                    }, 
                    })
                    .then(res => res.json())
                    .then(res => {
                            if(res.status === true){
                            alert(res.message)
                            getBooks();
                        }
            })
            .catch(err => alert("Error in getting data"));
            }


    return (
        <>
        {books && books !== [] ?
        books.map((m,i) => <BookItem key={i} data={m} deleteBook={deleteBook} />)
    : "" }

    <div className='d-flex justify-content-center'>
        <ReactPaginate
        previousLabel={'previus'}
        nextLabel={'next'}
        breakLabel={'...'}
        breakClassName={'page-link'}
        pageCount={booksCount}
        marginPagesDisplayed={2}
        pageRangeDisplayed={5}
        onPageChange={handlePageClick}
        containerClassName={'pagination'}
        pageClassName={'page-item'}
        pageLinkClassName={'page-link'}
        previousClassName={'page-link'}
        nextClassName={'page-link'}
        activeClassName={'active'}
        />
    </div>
        </>
    )
}

export default BookList;