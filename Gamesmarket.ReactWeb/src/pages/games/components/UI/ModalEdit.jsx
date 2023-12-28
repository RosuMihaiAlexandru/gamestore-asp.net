import React, { useState } from 'react';
import MyModal from './MyModal';
import MyInput from './input/MyInput';
import MyButton from './button/MyButton';
import ModalSelect from './select/ModalSelect';
import { GameGenreOptions } from '../../components/GameGenre';
import MyCreateInput from './input/MyCreateInput';
import { useGameHandlers } from '../../Utils/CRUDhandlers';

const ModalEdit = ({ visible, setVisible, selectedGame, handleEdit, handleEditChange}) => {
  const {handleImageChange} = useGameHandlers();
  const [previewImage, setPreviewImage] = useState(null);

  const handleSubmit = (e) => {
      e.preventDefault();
      handleEdit(selectedGame.id);
    };

  return (
    <MyModal visible={visible} setVisible={setVisible}>
      {selectedGame && (
        <form onSubmit={handleSubmit} className='row g-3'>
          <div className="col-md-6">
                <span>Name:</span>
                <MyInput 
                  type="text"
                  placeholder="Name"
                  name="name"
                  value={selectedGame.name}
                  onChange={(e) => handleEditChange(e, 'name')}
                />
          </div>
          <div className="col-md-6">
                <span>Developer:</span>
                <MyInput
                  type="text"
                  placeholder="Developer"
                  name="developer"
                  value={selectedGame.developer}
                  onChange={(e) => handleEditChange(e, 'developer')}
                />
          </div>
          <div className="col-md-6">
                <span>Description:</span>
                <MyInput
                  type="text"
                  placeholder="Description"
                  name="description"
                  value={selectedGame.description}
                  onChange={(e) => handleEditChange(e, 'description')}
                />
          </div>
          <div className="col-md-6">
                <span>Price:</span>
                <MyInput
                  type="number"
                  placeholder="Price"
                  name="price"
                  value={selectedGame.price}
                  onChange={(e) => handleEditChange(e, 'price')}
                />
          </div>
          <div className="col-md-6">
              <span>Release Date:</span>
              <MyInput
                type="date"
                placeholder="Release Date"
                name="releaseDate"
                value={selectedGame.releaseDate}
                onChange={(e) => handleEditChange(e, 'releaseDate')}
              />
          </div>
          <div className="col-md-6">
              <span>Game genre:</span>
              <ModalSelect
                id="gameGenre"
                name="gameGenre"
                value={selectedGame.gameGenre}
                onChange={(e) => handleEditChange(e, 'gameGenre')}
              options={GameGenreOptions}
            />
          </div>
          <span>Picture must be less than 2 megabyte and have format of .jpg .webp .png</span>
          <div className="col-md-6">
            <span>Image:</span>
            <MyCreateInput type="file" onChange={(e) => handleImageChange(e, setPreviewImage)} />
          </div>
          {previewImage && (
            <div className="text-center mt-2">
              <img
                src={previewImage}
                alt="Selected"
                style={{ maxWidth: '100%', maxHeight: '200px' }}
              />
            </div>
          )}
          <div className=" text-end">
            <MyButton type="submit">Update Game</MyButton>
          </div>
        </form>
      )}
    </MyModal>
  );
};
  
export default ModalEdit;

